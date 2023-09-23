using AutoMapper;
using FoodieLionApi.Models;
using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Models.Params;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodieLionApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<UserOutDto>>>> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        var res = new Result<List<UserOutDto>> { Code = ErrorCode.SUCCESS, Data = users };
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<UserOutDto>>> GetById(Guid id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            var res = new Result<UserOutDto> { Code = ErrorCode.SUCCESS, Data = user };
            return Ok(res);
        }
        catch (FoodieLionException e)
        {
            return BadRequest(new Result<UserOutDto> { Code = e.Code, Error = e.Message });
        }
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<Result<List<UserOutDto>>>> GetByName(string name)
    {
        var users = await _userService.GetUsersByNameAsync(name);
        var res = new Result<List<UserOutDto>> { Code = ErrorCode.SUCCESS, Data = users };
        return Ok(res);
    }

    [HttpGet("nameOrEmail")]
    public async Task<ActionResult<Result<UserOutDto>>> GetByNameOrEmail(string nameOrEmail)
    {
        try
        {
            var res = await _userService.GetUserByNameOrEmailAsync(nameOrEmail);
            return Ok(new Result<UserOutDto>(res));
        }
        catch (FoodieLionException e)
        {
            return BadRequest(new Result<UserOutDto>() { Code = e.Code, Error = e.Message });
        }
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<Result<UserOutDto>>> GetByEmail(string email)
    {
        try
        {
            var user = await _userService.GetUserByEmailAsync(email);
            var res = new Result<UserOutDto> { Code = ErrorCode.SUCCESS, Data = user };
            return Ok(res);
        }
        catch (FoodieLionException e)
        {
            return BadRequest(new Result<UserOutDto> { Code = e.Code, Error = e.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<Result<UserOutDto>>> Create([FromBody] UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        try
        {
            var createdUser = await _userService.CreateUserAsync(user);
            var res = new Result<UserOutDto> { Code = ErrorCode.SUCCESS, Data = createdUser };
            return Ok(res);
        }
        catch (FoodieLionException e)
        {
            return BadRequest(new Result<UserOutDto> { Code = e.Code, Error = e.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result<UserOutDto>>> Update(
        Guid id,
        [FromBody] UpdateUserParams user
    )
    {
        try
        {
            var updatedUser = await _userService.UpdateUserAsync(id, user);
            var res = new Result<UserOutDto> { Code = ErrorCode.SUCCESS, Data = updatedUser };
            return Ok(res);
        }
        catch (FoodieLionException e)
        {
            return BadRequest(new Result<UserOutDto> { Code = e.Code, Error = e.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result<UserOutDto>>> Delete(Guid id)
    {
        try
        {
            var deletedUser = await _userService.DeleteUserAsync(id);
            var res = new Result<UserOutDto> { Code = ErrorCode.SUCCESS, Data = deletedUser };
            return Ok(res);
        }
        catch (FoodieLionException e)
        {
            return BadRequest(new Result<UserOutDto> { Code = e.Code, Error = e.Message });
        }
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<Result<bool>>> VerifyName(string name)
    {
        var res = new Result<bool>
        {
            Code = ErrorCode.SUCCESS,
            Data = await _userService.VerifyName(name)
        };
        return Ok(res);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<Result<bool>>> VerifyEmail(string email)
    {
        var res = new Result<bool>
        {
            Code = ErrorCode.SUCCESS,
            Data = await _userService.VerifyEmail(email)
        };
        return Ok(res);
    }
}
