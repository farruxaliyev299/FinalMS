using AutoMapper;
using Dapper;
using FinalMS.Discount.DTOs;
using FinalMS.Discount.DTOs.Create;
using FinalMS.Discount.DTOs.Update;
using FinalMS.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Npgsql;
using System.Data;

namespace FinalMS.Discount.Services;

public class DiscountService : IDiscountService
{
    private readonly IMapper _mapper;
    private readonly IDbConnection _connection;
    private readonly IConfiguration _config;

    public DiscountService(IMapper mapper, IConfiguration config)
    {
        _mapper = mapper;
        _config = config;

        _connection = new NpgsqlConnection(_config.GetConnectionString("PostgreSql"));
    }

    public async Task<Response<List<DiscountDto>>> GetAll()
    {
        var discounts = await _connection.QueryAsync<Models.Discount>("select * from discounts");

        return Response<List<DiscountDto>>.Success(_mapper.Map<List<DiscountDto>>(discounts.ToList()), StatusCodes.Status200OK);
    }

    public async Task<Response<DiscountDto>> GetById(int id)
    {
        var discount = await _connection.QueryAsync<Models.Discount>("select * from discounts where Id = @Id", new { Id = id });

        var existingDiscount = discount.FirstOrDefault();

        if (existingDiscount == null) return Response<DiscountDto>.Fail("Discount not found", StatusCodes.Status404NotFound);

        return Response<DiscountDto>.Success(_mapper.Map<DiscountDto>(existingDiscount), StatusCodes.Status200OK);
    }

    public async Task<Response<DiscountDto>> GetByCodeAndUserId(string code, string userId)
    {
        var discounts = await _connection.QueryAsync<Models.Discount>("select * from discounts where Code = @Code and UserId = @UserId", new
        {
            Code = code,
            UserId = userId
        });

        var existingDiscount = discounts.FirstOrDefault();

        if (existingDiscount == null) return Response<DiscountDto>.Fail("Discount not found", StatusCodes.Status404NotFound);

        return Response<DiscountDto>.Success(_mapper.Map<DiscountDto>(existingDiscount), StatusCodes.Status200OK);
    }

    public async Task<Response<NoContent>> Create(DiscountCreateDto discountDto)
    {
        var createStatus = await _connection.ExecuteAsync("insert into discounts (UserId, Rate, Code) values(@UserId, @Rate, @Code)", new 
        {
            UserId = discountDto.UserId,
            Rate = discountDto.Rate,
            Code = discountDto.Code
        });

        if (!(createStatus > 0)) return Response<NoContent>.Fail("Problems occured while creating", StatusCodes.Status500InternalServerError);

        return Response<NoContent>.Success(StatusCodes.Status204NoContent);
    }

    public async Task<Response<NoContent>> Update(DiscountUpdateDto discountDto)
    {
        var updateStatus = await _connection.ExecuteAsync("update discounts set UserId = @UserId, Rate = @Rate, Code = @Code where Id = @Id", new
        {
            Id = discountDto.Id,
            UserId = discountDto.UserId,
            Rate = discountDto.Rate,
            Code = discountDto.Code
        });

        if(!(updateStatus > 0)) return Response<NoContent>.Fail("Discount not found", StatusCodes.Status404NotFound);

        return Response<NoContent>.Success(StatusCodes.Status204NoContent);
    }

    public async Task<Response<NoContent>> Delete(int id)
    {
        var deleteStatus = await _connection.ExecuteAsync("delete from discounts where Id = @id", new { Id = id });

        if(!(deleteStatus > 0)) return Response<NoContent>.Fail("Discount not found", StatusCodes.Status404NotFound);

        return Response<NoContent>.Success(StatusCodes.Status204NoContent);
    }
}
