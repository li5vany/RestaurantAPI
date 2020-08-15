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
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;


        public MenuController(IMenuService menuService)
        {
            _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService));
        }

        /// <summary>
        /// Get Menu By ID
        /// </summary>
        /// <param name="id">Menu ID</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenu(int id)
        {
            return Ok(await _menuService.Get(id));
        }
        
        /// <summary>
        /// Get Menu By Name
        /// </summary>
        /// <param name="id">Menu Name</param>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetMenuByName(string name)
        {
            return Ok(await _menuService.Get(name));
        }

        /// <summary>
        /// Get Menus
        /// </summary>
        /// <param name="skip">Skip</param>
        /// <param name="limit">Limit</param>
        /// <param name="filter">Filter</param>
        /// <param name="columnSort">Column Sort</param>
        /// <param name="descending">Is Descending</param>
        [HttpGet()]
        public async Task<IActionResult> GetMenus(int skip = 0, int limit = 10, string filter = "", string columnSort = "", bool descending = true)
        {
            Response.Headers.Add("X-Total-Count", (await _menuService.Count(filter)).ToString());
            Response.Headers.Add("X-Total-Limit", limit.ToString());
            Response.Headers.Add("X-Total-Skip", skip.ToString());

            HttpContext.Response.Headers["Access-Control-Expose-Headers"] = "X-Total-Count, X-Total-Limit, X-Total-Skip";

            return Ok(await _menuService.GetAll(skip, limit, filter, columnSort, descending));
        }
        
        /// <summary>
        /// Get Menus By Card ID
        /// </summary>
        /// <param name="id">Card ID</param>
        /// <param name="skip">Skip</param>
        /// <param name="limit">Limit</param>
        /// <param name="filter">Filter</param>
        /// <param name="columnSort">Column Sort</param>
        /// <param name="descending">Is Descending</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenusByCard(int id, int skip = 0, int limit = 10, string filter = "", string columnSort = "", bool descending = true)
        {
            Response.Headers.Add("X-Total-Count", (await _menuService.CountByCard(id, filter)).ToString());
            Response.Headers.Add("X-Total-Limit", limit.ToString());
            Response.Headers.Add("X-Total-Skip", skip.ToString());

            HttpContext.Response.Headers["Access-Control-Expose-Headers"] = "X-Total-Count, X-Total-Limit, X-Total-Skip";

            return Ok(await _menuService.GetAllByCard(id, skip, limit, filter, columnSort, descending));
        }

        /// <summary>
        /// Add Menu
        /// </summary>
        /// <param name="dto">Menu Request DTO</param>
        [HttpPost()]
        public async Task<IActionResult> AddMenu([FromBody] MenuRequestDTO dto)
        {
            await _menuService.Add(dto);

            return Ok();
        }

        /// <summary>
        /// Update Menu
        /// </summary>
        /// <param name="dto">Menu Request DTO</param>
        /// <param name="id">Menu ID</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu([FromBody] MenuRequestDTO dto, int id)
        {
            await _menuService.Update(dto, id);

            return Ok();
        }

        /// <summary>
        /// Change Active
        /// </summary>
        /// <param name="isActive">Is Active</param>
        /// <param name="id">Menu ID</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeActive([FromBody] bool isActive, int id)
        {
            await _menuService.ChangeActive(id, isActive);

            return Ok();
        }

        /// <summary>
        /// Delete Menu
        /// </summary>
        /// <param name="id">Menu ID</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            await _menuService.Delete(id);

            return Ok();
        }

    }
}
