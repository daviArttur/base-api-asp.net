using Testes.src.domain.entities;

namespace Testes.src.app.interfaces
{
    public interface IToolRepository
    {
        public Task Save(Tool tool);
        public Task<List<Tool>> FindByTag(string tag);
        public Task<List<Tool>> FindAll();
    }
}
