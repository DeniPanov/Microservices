using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext context;

        public PlatformRepo(AppDbContext context)
        {
            this.context = context;
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            this.context.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return this.context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return this.context.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (this.context.SaveChanges() >= 0);
        }
    }
}
