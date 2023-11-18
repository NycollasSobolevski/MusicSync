using music_api;
using music_api.DTO;
using music_api.Model;

public static class UserTools
{
    public static async Task<User> ValidateUser( IRepository<User> userRepository, IJwtService jwtService, string jwtString  )
    {
        var jwtUser = jwtService.Validate<UserJwtData>(jwtString);
        var user = await userRepository.FirstOrDefaultAsync(user =>
            user.Name == jwtUser.Name
        );

        if (user == null)
            throw new Exception("User not found");

        return user;

    }
}