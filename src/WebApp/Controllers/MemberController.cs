using System;
using System.Threading.Tasks;
using Codidact.Core.Application.Members;
using Codidact.Core.Domain.Common;
using Codidact.Core.Domain.Entities;
using Codidact.Core.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Codidact.Core.WebApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IMembersRepository _memberRepository;

        public MemberController(
            ILogger<MemberController> logger,
            IMembersRepository membersRepository)
        {
            _logger = logger;
            _memberRepository = membersRepository;
        }

        [HttpPost("create")]
        public async Task<EntityResult<Member>> Create([FromBody]MemberRequest request)
        {
            _logger.LogDebug(@$"{DateTime.Now.ToString()}: 
                Received Request for Create User:
                {nameof(request.DisplayName)} {request.DisplayName}
                {nameof(request.UserId)} {request.UserId}");
            var result = await _memberRepository.Create(
                new Member
                {
                    DisplayName = request.DisplayName,
                    UserId = request.UserId,
                });

            return result;
        }
    }
}
