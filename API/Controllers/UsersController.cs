using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{

    private readonly IUserRepository _userRepository;

    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await _userRepository.GetMembersAsync();

        return Ok(users);
    }

    [HttpGet("{id:int}")] // /api/users/{id}
    public async Task<ActionResult<MemberDto>> GetUserById(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        return _mapper.Map<MemberDto>(user);
    }

    [HttpGet("{username}")] // /api/users/{id}
    public async Task<ActionResult<MemberDto>> GetUserByUsername(string username)
    {
        return await _userRepository.GetMemberAsync(username);
    }

}
