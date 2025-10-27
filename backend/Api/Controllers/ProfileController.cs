

using Application.Photo.Command;
using Application.Photo.Query;
using Application.UserFollowing.Command;
using Application.UserFollowing.Query;
using Application.Users.Query;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly UserManager<User> userManager;

        public ProfileController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile userPhoto, CancellationToken token)
        {

            var stream = userPhoto.OpenReadStream();
            var result = await this.Mediator.Send(new PhotoCommandRequest { PhotoStream = stream }, token);
            var user = await userManager.GetUserAsync(this.HttpContext.User);
            user!.ImageUrl = result.Value?[0].Url;
            await this.userManager.UpdateAsync(user!);
            return this.ReturnResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> UserImages(Guid id, CancellationToken token)
        {
            var result = await this.Mediator.Send(new PhotoQueryRequest { UserId = id }, token);
            return this.ReturnResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteImage([FromQuery] Guid imageId, CancellationToken token)
        {
            var result = await this.Mediator.Send(new PhotoDeleteRequest { ImageId = imageId }, token);
            return this.ReturnResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(new UserProfileQueryRequest { Id = id }, cancellationToken);
            return this.ReturnResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> FollowersUpdate(string targetId, bool isFollowing, CancellationToken token)
        {
            var result = await this.Mediator.Send(new UserFollowingCommandRequest
            { TargetId = targetId, IsFollowing = isFollowing }, token);
            return this.ReturnResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetFollowers([FromQuery] string userId, CancellationToken token)
        {
            var result = await this.Mediator.Send(new FollowersRequest
            { UserId = userId }, token);
            return this.ReturnResult(result);
        }

        /// <summary>
        /// Gets the list of users, whom userid is following
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns> 
        [HttpGet]
        public async Task<IActionResult> GetFollowing([FromQuery] string userId, CancellationToken token)
        {
            var result = await this.Mediator.Send(new FollowingRequest
            { UserId = userId }, token);
            return this.ReturnResult(result);
        }
    }
}