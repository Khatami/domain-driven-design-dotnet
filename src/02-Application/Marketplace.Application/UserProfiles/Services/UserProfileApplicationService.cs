﻿using Marketplace.Application.Contracts.UserProfiles.Commands.V1;
using Marketplace.Application.Contracts.UserProfiles.IServices;
using Marketplace.Application.Infrastructure;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles;
using Marketplace.Domain.UserProfiles.Delegates;
using Marketplace.Domain.UserProfiles.ValueObjects;

namespace Marketplace.Application.UserProfiles.Services
{
	internal class UserProfileApplicationService : IUserProfileApplicationService
	{
		private readonly IUserProfileRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly CheckTextForProfanity _checkText;

		public UserProfileApplicationService(IUserProfileRepository repository,
			IUnitOfWork unitOfWork,
			CheckTextForProfanity checkText)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
			_checkText = checkText;
		}

		public async Task Handle(object command)
		{
			switch (command)
			{
				case RegisterUserCommand cmd:
					if (await _repository.Exists(new UserId(cmd.UserId)))
						throw new InvalidOperationException($"Entity with id {cmd.UserId} already exists");

					var userProfile = new UserProfile(new UserId(cmd.UserId),
						FullName.FromString(cmd.FullName),
						DisplayName.FromString(cmd.DisplayName, _checkText));

					await _repository.Add(userProfile);
					await _unitOfWork.Commit();
					break;

				case UpdateUserFullNameCommand cmd:
					await HandleUpdate(cmd.UserId, profile => profile.UpdateFullName(FullName.FromString(cmd.FullName)));
					break;

				case UpdateUserDisplayNameCommand cmd:
					await HandleUpdate(cmd.UserId, profile => profile.UpdateDisplayName(DisplayName.FromString(cmd.DisplayName, _checkText)));
					break;

				case UpdateUserProfilePhotoCommand cmd:
					await HandleUpdate(cmd.UserId, profile => profile.UpdateProfilePhoto(new Uri(cmd.PhotoUrl)));
					break;

				default:
					throw new InvalidOperationException($"Command type {command.GetType().FullName} is unknown");
			}
		}

		private async Task HandleUpdate(Guid userProfileId, Action<UserProfile> operation)
		{
			var userProfile = await _repository.Load(new UserId(userProfileId));

			if (userProfile == null)
				throw new InvalidOperationException($"Entity with id {userProfileId} cannot be found");

			operation(userProfile);

			await _unitOfWork.Commit();
		}
	}
}