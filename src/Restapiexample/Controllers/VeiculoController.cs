using System.Collections.Generic;
using System.Threading.Tasks;
using JHipsterNet.Pagination;
using JHipsterNet.Pagination.Extensions;
using Compuletra.RestApiExample.Data;
using Compuletra.RestApiExample.Data.Extensions;
using Compuletra.RestApiExample.Models;
using Compuletra.RestApiExample.Web.Extensions;
using Compuletra.RestApiExample.Web.Filters;
using Compuletra.RestApiExample.Web.Rest.Problems;
using Compuletra.RestApiExample.Web.Rest.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Compuletra.RestApiExample.Controllers {
    [Authorize]
    [Route("api")]
    [ApiController]
    public class VeiculoController : ControllerBase {
        private const string EntityName = "veiculo";

        private readonly ApplicationDatabaseContext _applicationDatabaseContext;

        private readonly ILogger<VeiculoController> _log;

        public VeiculoController(ILogger<VeiculoController> log,
            ApplicationDatabaseContext applicationDatabaseContext)
        {
            _log = log;
            _applicationDatabaseContext = applicationDatabaseContext;
        }

        [HttpPost("veiculos")]
        [ValidateModel]
        public async Task<ActionResult<Veiculo>> CreateVeiculo([FromBody] Veiculo veiculo)
        {
            _log.LogDebug($"REST request to save Veiculo : {veiculo}");
            if (veiculo.Id != 0)
                throw new BadRequestAlertException("A new veiculo cannot already have an ID", EntityName, "idexists");
            _applicationDatabaseContext.Veiculos.Add(veiculo);
            await _applicationDatabaseContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVeiculo), new { id = veiculo.Id }, veiculo)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, veiculo.Id.ToString()));
        }

        [HttpPut("veiculos")]
        [ValidateModel]
        public async Task<IActionResult> UpdateVeiculo([FromBody] Veiculo veiculo)
        {
            _log.LogDebug($"REST request to update Veiculo : {veiculo}");
            if (veiculo.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            //TODO catch //DbUpdateConcurrencyException into problem
            _applicationDatabaseContext.Update(veiculo);
            await _applicationDatabaseContext.SaveChangesAsync();
            return Ok(veiculo)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, veiculo.Id.ToString()));
        }

        [HttpGet("veiculos")]
        public ActionResult<IEnumerable<Veiculo>> GetAllVeiculos(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Veiculos");
            var page = _applicationDatabaseContext.Veiculos
                .UsePageable(pageable);
            return Ok(page.Content).WithHeaders(page.GeneratePaginationHttpHeaders());
        }

        [HttpGet("veiculos/{id}")]
        public async Task<IActionResult> GetVeiculo([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Veiculo : {id}");
            var result = await _applicationDatabaseContext.Veiculos
                .SingleOrDefaultAsync(veiculo => veiculo.Id == id);
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("veiculos/{id}")]
        public async Task<IActionResult> DeleteVeiculo([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Veiculo : {id}");
            _applicationDatabaseContext.Veiculos.RemoveById(id);
            await _applicationDatabaseContext.SaveChangesAsync();
            return Ok().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
