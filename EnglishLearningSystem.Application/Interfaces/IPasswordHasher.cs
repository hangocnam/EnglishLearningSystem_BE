using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearningSystem.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string GeneratePasswordHashed(string password);
    }
}
