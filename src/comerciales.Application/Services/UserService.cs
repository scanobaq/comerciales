
using comerciales.Application.Abstractions;
using comerciales.Domain.Interfaces;


namespace comerciales.Application.Services;

public class UserService(IJwtTokenGenerator jwtTokenGenerator,
IPasswordHasher hasher,
IUserRepository userRepository) : IUserService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IPasswordHasher _hasher = hasher;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<string> AuthenticateAsync(string email, string password)
    {

        //Validamos si el usuario existe
        var user = await _userRepository.UserExistsAsync(email);
        if (user == null)
            throw new Exception("El usuario no existe");

        // Validamos la contraseña
        var isValidPassword = _hasher.Verify(password, user.PasswordHash);
        if (!isValidPassword)
            throw new Exception("La contraseña es incorrecta");

        // Generamos el token JWT
        var token = _jwtTokenGenerator.GenerateToken(user.UsuarioId, user.Nombre, user.Correo, user.Rol);

        return token;
    }
}

