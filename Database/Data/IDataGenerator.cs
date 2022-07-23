
using Microsoft.EntityFrameworkCore;

namespace Database.Data {

    public interface IDataGenerator {
        
        public void Generate(ModelBuilder builder);
    }
}