using BeautySalonApp.Data;
using BeautySalonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonApp.Services
{
    public class BranchService
    {
        private readonly GlobalDbContext _globalContext;

        public BranchService(GlobalDbContext globalContext)
        {
            _globalContext = globalContext;
        }

        public List<Branch> GetBranches()
        {
            return _globalContext.Branches.ToList();
        }
    }
}
