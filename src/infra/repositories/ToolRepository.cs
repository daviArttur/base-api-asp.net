using Testes.src.infra.config.db;
using Testes.src.domain.entities;
using Testes.src.app.interfaces;
using Testes.src.infra.models;
using Testes.src.domain.dto;
using Microsoft.EntityFrameworkCore;

namespace Testes.src.infra.repositories
{
    public class ToolRepository(AppDbContext context) : IToolRepository
    {
        private AppDbContext _context = context;

        public async Task Save(Tool tool)
        {
            try
            {
                ToolModel toolModel = new ToolModel()
                {
                    Title = tool.Title,
                    Description = tool.Description,
                    Link = tool.Link,
                    Id = tool.Id,
                    UserId = tool.UserId,
                    Tags = {
                    new TagModel{Value = "123"}
                }
                };
                await _context.Tools.AddAsync(toolModel);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new QueryException();
            }
        }

        public async Task<List<Tool>> FindAll()
        {
            try
            {
                var toolsModel = await _context.Tools.Include(t => t.Tags).ToListAsync();
                List<Tool> tools = this._convertToolModelForTool(toolsModel);
                return tools;
            }
            catch
            {
                throw new QueryException();
            }
        }

        public async Task<List<Tool>> FindByTag(string tag)
        {
            try
            {
                var toolsIdWithTag = this._context.Tags
                    .Where(t => t.Value == tag)
                    .Select(t => t.ToolId);
                var result = await this._context.Tools
                    .Where(t => toolsIdWithTag.Contains(t.Id))
                    .Include(t => t.Tags) // Certifique-se de incluir a relação das tags na entidade Tool, se existir
                    .ToListAsync();
                return this._convertToolModelForTool(result);
            }
            catch
            {
                throw new QueryException();
            }
        }

        private List<Tool> _convertToolModelForTool(List<ToolModel> toolsModel)
        {
            try
            {
                List<Tool> tools = [];
                for (int i = 0; i < toolsModel.Count; i++)
                {
                    var currentToolModel = toolsModel[i];
                    List<string> tags = [];
                    foreach (var tag in currentToolModel.Tags)
                    {
                        tags.Add(tag.Value);
                    }
                    CreateToolDto createToolDto = new CreateToolDto
                    {
                        Id = currentToolModel.Id,
                        Title = currentToolModel.Title,
                        Link = currentToolModel.Link,
                        Description = currentToolModel.Description,
                        Tags = tags,
                        UserId = currentToolModel.UserId,
                    };
                    var tool = new Tool(createToolDto);
                    tools.Add(tool);
                }
                return tools;
            }
            catch
            {
                throw new QueryException();
            }
        }
    }
}

