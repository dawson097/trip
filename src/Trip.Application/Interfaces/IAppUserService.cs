using Microsoft.AspNetCore.Identity;
using Trip.Application.Dtos.AppUser;
using Trip.Domain.Entities;

namespace Trip.Application.Interfaces;

/// <summary>
/// 用户业务逻辑
/// </summary>
public interface IAppUserService : ICommonService<AppUser>
{
    /// <summary>
    /// 根据传入的DTO数据，校验是否完成登录
    /// </summary>
    /// <param name="userLoginDto">用户登录DTO</param>
    /// <returns>完成返回succeed，未完成返回failed</returns>
    Task<SignInResult> CheckLoginAsync(AppUserLoginDto userLoginDto);

    /// <summary>
    /// 获取token字符串
    /// </summary>
    /// <param name="userLoginDto">用户登录字符串</param>
    /// <returns>完成JWT加工的token字符串</returns>
    Task<string> GetTokenStringAsync(AppUserLoginDto userLoginDto);

    /// <summary>
    /// 注册新用户
    /// </summary>
    /// <param name="userRegisterDto">用户注册字符串</param>
    /// <returns>注册成功返回succeed，失败返回failed</returns>
    Task<(IdentityResult, AppUser)> RegisterAsync(AppUserRegisterDto userRegisterDto);

    /// <summary>
    /// 完成用户注册后，创建对应的购物车
    /// </summary>
    /// <param name="user">注册好的用户</param>
    Task CreateShopCart(AppUser user);
}