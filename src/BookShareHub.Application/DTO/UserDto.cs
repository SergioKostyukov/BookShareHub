namespace BookShareHub.Application.Dto;

public record UserDto(
	string Id, 
	string UserName, 
	float? Rating
);
