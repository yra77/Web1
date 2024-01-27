

namespace Web1_Server.Services.HashService
{
	public interface IHash
	{
        string ComputeHash(string password, string salt, int iteration);
    }
}

