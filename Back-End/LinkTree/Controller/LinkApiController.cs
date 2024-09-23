using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkTree.Application.DTOs;
using LinkTree.Application.Services.Interfaces;
using LinkTree.Domain.Exceptions;
using LinkTree.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkTree.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinkApiController : ControllerBase
    {
        private readonly ILinksService _linkService;

        public LinkApiController(ILinksService linksService){
            _linkService = linksService;
        }



        [HttpGet("Links/Get-Links/{id}")]
        public async Task<IActionResult> Get(int? id){
            try{
                var link = await _linkService.GetLinks(id);
                return Ok(link);

            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Links/Create-Links/")]
        public async Task<IActionResult> Create([FromBody] LinksDTO? linksDTO){
            try{
                await _linkService.addLinks(linksDTO);
                return Ok("Success");

            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Links/Update-Links/")]
        public async Task<IActionResult> Update([FromBody] LinkModel? link){
            try{
                await _linkService.updateLinks(link);
                return Ok("Success");

            }catch(Exception ex){
               return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Links/Delete-Links/{id}")]
        public async Task<IActionResult> Delete(int? id){
            try{
                await _linkService.removeLinks(id);
                return Ok("Success");

            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}