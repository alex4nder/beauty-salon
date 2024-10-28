using BeautySalonApp.Data;
using BeautySalonApp.Models;

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

        private DatabaseService _databaseService;
        private LocalDbContext _context;
        private readonly CurrentBranchContext _CurrentBranchContext;

        public Branch GetBranchById(int branchId)
        {
            return _globalContext.Branches.Find(branchId);
        }

        public void BranchEdit(Branch Branch)
        {
            var existingBranch = _globalContext.Branches.Find(Branch.Id);
            if (existingBranch != null)
            {
                existingBranch.Title = Branch.Title;
                existingBranch.Location = Branch.Location;
                existingBranch.Phone = Branch.Phone;

                _globalContext.SaveChanges();
            }
        }
    }
}
