using MageVsMonsters.Models;

namespace MageVsMonsters.Views
{
    public interface IView<T> : IBaseView where T : IModel
    {
    }
}