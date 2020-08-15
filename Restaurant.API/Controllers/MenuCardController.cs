using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Common.DTO.Responses;
using Microsoft.AspNetCore.Cors;
using Restaurant.Services.Interfaces;
using Restaurant.Common.DTO.Requests;

namespace Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors("EnableAnyRequest")]
    public class MenuCardController : ControllerBase
    {
        private readonly IMenuCardService _menuService;


        public MenuCardController(IMenuCardService menuService)
        {
            _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService));
        }

        /// <summary>
        /// Get Menu Card By ID
        /// </summary>
        /// <param name="id">MenuCard ID</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuCard(int id)
        {
            return Ok(await _menuService.Get(id));
        }

        /// <summary>
        /// Get Menu Card By Name
        /// </summary>
        /// <param name="id">MenuCard Name</param>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetMenuCardByName(string name)
        {
            return Ok(await _menuService.Get(name));
        }

        /// <summary>
        /// Get Menu Card By ID With Menu
        /// </summary>
        /// <param name="id">MenuCard ID</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuCardWithMenu(int id)
        {
            return Ok(await _menuService.GetWithMenu(id));
        }

        /// <summary>
        /// Get Menu Card By Name With Menu
        /// </summary>
        /// <param name="id">MenuCard Name</param>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetMenuCardByNameWithMenu(string name)
        {
            return Ok(await _menuService.GetWithMenu(name));
        }

        /// <summary>
        /// Get Menu Cards
        /// </summary>
        /// <param name="skip">Skip</param>
        /// <param name="limit">Limit</param>
        /// <param name="filter">Filter</param>
        /// <param name="columnSort">Column Sort</param>
        /// <param name="descending">Is Descending</param>
        [HttpGet()]
        public async Task<IActionResult> GetMenuCards(int skip = 0, int limit = 10, string filter = "", string columnSort = "", bool descending = true)
        {
            Response.Headers.Add("X-Total-Count", (await _menuService.Count(filter)).ToString());
            Response.Headers.Add("X-Total-Limit", limit.ToString());
            Response.Headers.Add("X-Total-Skip", skip.ToString());

            HttpContext.Response.Headers["Access-Control-Expose-Headers"] = "X-Total-Count, X-Total-Limit, X-Total-Skip";

            return Ok(await _menuService.GetAll(skip, limit, filter, columnSort, descending));
        }

        /// <summary>
        /// Get Menu Cards By Card ID With Menu
        /// </summary>
        /// <param name="skip">Skip</param>
        /// <param name="limit">Limit</param>
        /// <param name="filter">Filter</param>
        /// <param name="columnSort">Column Sort</param>
        /// <param name="descending">Is Descending</param>
        [HttpGet()]
        public async Task<IActionResult> GetMenuCardsWithMenu(int skip = 0, int limit = 10, string filter = "", string columnSort = "", bool descending = true)
        {
            Response.Headers.Add("X-Total-Count", (await _menuService.Count(filter)).ToString());
            Response.Headers.Add("X-Total-Limit", limit.ToString());
            Response.Headers.Add("X-Total-Skip", skip.ToString());

            HttpContext.Response.Headers["Access-Control-Expose-Headers"] = "X-Total-Count, X-Total-Limit, X-Total-Skip";

            return Ok(await _menuService.GetAllWithMenus(skip, limit, filter, columnSort, descending));
        }

        /// <summary>
        /// Add Menu Card
        /// </summary>
        /// <param name="dto">MenuCard Request DTO</param>
        [HttpPost()]
        public async Task<IActionResult> AddMenuCard([FromBody] MenuCardRequestDTO dto)
        {
            await _menuService.Add(dto);

            return Ok();
        }

        /// <summary>
        /// Update Menu Card
        /// </summary>
        /// <param name="dto">MenuCard Request DTO</param>
        /// <param name="id">MenuCard ID</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuCard([FromBody] MenuCardRequestDTO dto, int id)
        {
            await _menuService.Update(dto, id);

            return Ok();
        }

        /// <summary>
        /// Change Active
        /// </summary>
        /// <param name="isActive">Is Active</param>
        /// <param name="id">MenuCard ID</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeActive([FromBody] bool isActive, int id)
        {
            await _menuService.ChangeActive(id, isActive);

            return Ok();
        }

        /// <summary>
        /// Delete Menu Card
        /// </summary>
        /// <param name="id">MenuCard ID</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuCard(int id)
        {
            await _menuService.Delete(id);

            return Ok();
        }

    }
}
