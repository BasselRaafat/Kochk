using System.Security.Claims;
using AutoMapper;
using Kochk.Application.Common.Models;
using Kochk.Application.Features.Auth.Commands.CustomerSignUp;
using Kochk.Application.Features.Auth.Commands.SignIn;
using Kochk.Application.Features.Auth.Commands.VendorSignUP;
using Kochk.Application.Features.Auth.Models;
using Kochk.Application.Features.Auth.Queries;
using Kochk.Application.Features.User.Commands;
using Kochk.Application.Features.User.DTOs;
using Kochk.Application.Features.User.Queries;
using Kochk.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kochk.API.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    // private readonly SignInManager<AppUser> _signInManager;
    // private readonly IGenericRepository<BussnissUser> _bussnessisUserRepo;
    // private readonly IGenericRepository<UserAddress> _userAddressRepo;
    // private readonly IJwtTokenGenerator _jwtTokenGenerator;
    // private readonly JwtTokenGenerator _tokenGenerator;
    // private readonly IMapper _mapper;

    public AccountController(
        UserManager<AppUser> userManager,
        ISender sender,
        IMapper mapper
    // ,SignInManager<AppUser> signInManager,
    // IJwtTokenGenerator authService,
    // JwtTokenGenerator tokenGenerator,
    // IMapper mapper,
    // IGenericRepository<BussnissUser> bussnessisUserRepo,
    // IGenericRepository<UserAddress> userAddressRepo
    )
    {
        _userManager = userManager;
        // _signInManager = signInManager;
        // _jwtTokenGenerator = authService;
        // _tokenGenerator = tokenGenerator;
        // _mapper = mapper;
        // _bussnessisUserRepo = bussnessisUserRepo;
        // _userAddressRepo = userAddressRepo;
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("signin")]
    public async Task<ActionResult<SignInResponse>> SignIn(SignInRequest model)
    {
        SignInCommand command = _mapper.Map<SignInCommand>(model);
        var result = await _sender.Send(command);
        return result.Match(onSuccess: Ok, onFailure: HandleError);
        // var user = await _userManager.FindByEmailAsync(model.Email);
        // var roles = await _userManager.GetRolesAsync(user!);
        // if (user is null)
        //     return Unauthorized(new ApiError(401));
        // var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        // if (!result.Succeeded)
        //     return Unauthorized(new ApiError(401));
        // return Ok(
        //     new SignInResponse()
        //     {
        //         Email = user.Email!,
        //         Token = await _tokenGenerator.GenerateTokenAsync(user, roles),
        //     }
        // );
    }

    [HttpPost("signup/vendor")]
    public async Task<ActionResult<SignInResponse>> SignUp(VendorSignUpRequest model)
    {
        VendorSignUpCommand command = _mapper.Map<VendorSignUpCommand>(model);
        Result result = await _sender.Send(command);
        return result.Match(onSuccess: NoContent, onFailure: HandleError);
    }

    [HttpPost("signup/customer")]
    public async Task<ActionResult<SignInResponse>> SignUp(CustomerSignUpRequest model)
    {
        CustomerSignUpCommand command = _mapper.Map<CustomerSignUpCommand>(model);
        Result result = await _sender.Send(command);
        return result.Match(onSuccess: NoContent, onFailure: HandleError);
        // if (CheckExistingEmail(model.Email).Result.Value)
        // {
        //     return BadRequest(
        //         new ApiValidationError()
        //         {
        //             Errors = { ["email"] = new List<string>(["Email already taken"]) },
        //         }
        //     );
        // }
        //
        // var user = new AppUser()
        // {
        //     Email = model.Email,
        //     UserName = model.Email.Split("@")[0],
        //     PhoneNumber = model.PhoneNumber,
        // };
        // var result = await _userManager.CreateAsync(user, model.Password);
        // if (!result.Succeeded)
        //     return BadRequest(new ApiError(400));
        // return Ok(
        //     new UserDto()
        //     {
        //         Email = user.Email,
        //         Token = await _authService.GenerateTokenAsync(user, _userManager),
        //     }
        // );
    }

    [Authorize]
    [HttpGet("CuerrentUser")]
    public async Task<ActionResult<SignInResponse>> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var query = new CurrentUserQuery(new Guid(userId!));
        var result = await _sender.Send(query);
        return result.Match(onSuccess: Ok, onFailure: HandleError);

        // var email = User.FindFirstValue(ClaimTypes.Email);
        // var user = await _userManager.FindByEmailAsync(email!);
        //
        // var roles = await _userManager.GetRolesAsync(user!);
        // return Ok(
        //     new SignInResponse()
        //     {
        //         Email = user!.Email!,
        //         Token = await _jwtTokenGenerator.GenerateTokenAsync(user, roles),
        //     }
        // );
    }

    [Authorize]
    [HttpGet("UserAddress")]
    public async Task<ActionResult<IReadOnlyList<UserAddressUpdateRequest>>> GetAddress()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        GetUserAddressesQuery query = new(new Guid(userId!));
        var result = await _sender.Send(query);
        return result.Match(Ok, HandleError);

        // var user = await _bussnessisUserRepo.GetByIdAsync(new Guid(userId!));
        // var mappedaddres = _mapper.Map<IReadOnlyList<UserAddress>, IReadOnlyList<UserAddressDto>>(
        //     user!.Addresses.ToList()
        // );
        // return Ok(mappedaddres);
    }

    [Authorize]
    [HttpPut("Address/{addressId}")]
    public async Task<ActionResult<UserAddressUpdateRequest>> UpdateAddres(
        [FromRoute] Guid addressId,
        [FromBody] UserAddressUpdateRequest userAddress
    )
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var command = new UpdateUserAddressCommand(
            new Guid(userId!),
            addressId,
            userAddress.Appartment,
            userAddress.Street,
            userAddress.City,
            userAddress.Country
        );
        var result = await _sender.Send(command);
        return result.Match(onSuccess: NoContent, onFailure: HandleError);
        // var address = await _userAddressRepo.GetByIdAsync(addressId);
        // var mappedAddres = _mapper.Map<UserAddressUpdateRequest, UserAddress>(userAddress);
        // mappedAddres.Id = address!.Id;
        // _userAddressRepo.Update(mappedAddres);
        // return Ok(userAddress);
    }

    [HttpGet("EmailExist")]
    public async Task<ActionResult<bool>> CheckExistingEmail([FromQuery] string email)
    {
        return await _userManager.FindByEmailAsync(email) is not null;
    }
}
