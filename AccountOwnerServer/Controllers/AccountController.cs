using AutoMapper;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AccountOwnerServer.Controllers
{
    [Route("api/owner/{ownerId}/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public AccountController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountsForOwner(string ownerId, [FromQuery] AccountParameters parameters)
        {
            var accounts = await _repository.Account.GetAccountsByOwner(ownerId, parameters);

            var metadata = new
            {
                accounts.TotalCount,
                accounts.PageSize,
                accounts.CurrentPage,
                accounts.TotalPages,
                accounts.HasNext,
                accounts.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned {accounts.TotalCount} owners from database.");

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountForOwner(string ownerId, string id)
        {
            var account = _repository.Account.GetAccountByOwner(ownerId, id);

            if (account == null)
            {
                _logger.LogError($"Account with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            return Ok(account);
        }
    }
}
