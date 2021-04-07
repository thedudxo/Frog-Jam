using System.Collections.Generic;

namespace Chaseables
{
    public interface IChaseableCollection
    {
        List<IChaseable> GetAll();

        /// <returns>The IChasable with the smallest X positon whos CanChase = true</returns>
        IChaseable GetLastCanChase();
    }
}
