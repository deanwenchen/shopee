namespace AdminAPI.Features.Admins.Services;

public interface IAdminService
{
    Task<AdminListResponse> GetListAsync(int page, int pageSize);
    Task<AdminDto?> GetByIdAsync(int id);
    Task<AdminDto> CreateAsync(CreateAdminRequest request);
    Task<AdminDto?> UpdateAsync(int id, UpdateAdminRequest request);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateStatusAsync(int id, int status);
    Task<bool> ResetPasswordAsync(int adminId, int targetAdminId, string newPassword);
}
