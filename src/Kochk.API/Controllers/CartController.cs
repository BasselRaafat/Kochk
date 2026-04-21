using AutoMapper;
using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Interfaces.UnitOfWork;
using Kochk.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Kochk.API.Controllers;

public class CartController : BaseApiController
{
    private readonly IWriteRepository<Cart> _cartRepository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CartController(
        IWriteRepository<Cart> cartRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _cartRepository = cartRepository;
        this.mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    // [HttpGet]
    // public async Task<ActionResult<Cart>> GetCart([FromQuery] Guid id)
    // {
    //     var result = await _cartRepository.GetByIdAsync(id);
    //     return Ok(result);
    // }

    // [HttpPost]
    // public async Task<ActionResult> Creat([FromBody] CartDto cart)
    // {
    //     var mappedCart = mapper.Map<CartDto, Cart>(cart);
    //     _cartRepository.Update(mappedCart);
    //     return Ok();
    // }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteCart([FromRoute] Guid id)
    {
        var cart = await _cartRepository.GetByIdAsync(id);
        _cartRepository.Delete(cart!);
        var result = await _unitOfWork.SaveChangesAsync();
        return result != 0;
    }
}
