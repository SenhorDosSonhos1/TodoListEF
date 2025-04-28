using Microsoft.AspNetCore.Mvc;
using TodoListEF.Models;
using TodoListEF.Data;
using System.Linq;

namespace TodoListEF.Controllers
{
    public class TarefaController : Controller
    {
        private readonly AppDbContext _context;

        public TarefaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tarefas = _context.Tarefas.ToList();
            return View(tarefas);

        }

        [HttpPost]
        public IActionResult Criar(string nome)
        {
            var tarefa = new Tarefa { Nome = nome, Concluido = false };
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Concluir(int id)
        {
            var tarefa = _context.Tarefas.FirstOrDefault(x => x.Id == id);
            if (tarefa != null)
            {
                tarefa.Concluido = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Deletar(int id)
        {
            var tarefa = _context.Tarefas.FirstOrDefault(x => x.Id == id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}