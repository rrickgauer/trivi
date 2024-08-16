

export const API_PREFIX = '/api';


export class ApiEndpoints
{
    public static readonly Login = `${API_PREFIX}/auth/login`;
    public static readonly Signup = `${API_PREFIX}/auth/signup`;
    public static readonly Collections = `${API_PREFIX}/collections`;
    public static readonly Questions = `${API_PREFIX}/questions`;
    public static readonly Games = `${API_PREFIX}/games`;
    public static readonly Players = `${API_PREFIX}/players`;
    public static readonly Responses = `${API_PREFIX}/responses`;
}
