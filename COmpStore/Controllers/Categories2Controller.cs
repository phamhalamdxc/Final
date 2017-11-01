using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using COmpStore.Services;
using COmpStore.Schema.Entities;
using COmpStore.QueryParameters;
using AutoMapper;
using COmpStore.Schema.Dtos;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace COmpStore.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "resourcesAdmin")]
    public class Categories2Controller : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private ICategoryService _categoryService;

        public Categories2Controller(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<Category>), 200)]
        public IActionResult GetAllCategory(CategoryQueryParameters categoryQueryParameters)
        {
            var allCategories = _categoryService.GetAll(categoryQueryParameters).ToList();

            var allCategoriesDto = allCategories.Select(x => Mapper.Map<CategoryDto>(x));
            Response.Headers.Add("X-Pagination",
                            JsonConvert.SerializeObject(new { totalCount = _categoryService.Count() }));

            return Ok(allCategoriesDto);
        }
    }
}