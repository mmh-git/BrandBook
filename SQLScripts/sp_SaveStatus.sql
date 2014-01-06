USE [brandbook]
GO
/****** Object:  StoredProcedure [dbo].[StatusUPdate_SaveStatus]    Script Date: 12/25/2013 17:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[StatusUPdate_SaveStatus]
(
@StatusID int output,
@UserID uniqueidentifier,
@UserDetailsID int,
@StatusType varchar(10),
@StatusContent nvarchar(MAX),
@CreatedDate datetime
)
as

select @UserDetailsID=UserDetailsID from UserDetails ud
join
aspnet_Membership am
on ud.UserId=am.UserId
where am.UserId=@UserID

IF @StatusType='T'
Begin
	insert into StatusUpdate(UserDetailsID,StatusType,StatusContent,createdDate)
	values
	(@UserDetailsID,@StatusType,@StatusContent,@CreatedDate)
	select @StatusID=@@IDENTITY	
	select su.StatusID as StatusID,
su.StatusType as StatusType,
su.StatusContent as StatusContent,
su.createdDate as CreatedDate,
su.UserDetailsID as StatusByUserID,
ud.FirstName+' '+ud.LastName as FullName,
ud.proPicId as PicID,
img.ImageUrl as ProPicUrl from StatusUpdate as su
join
UserDetails as ud
on su.UserDetailsID=ud.UserDetailsID
join Images as img
on ud.proPicId=img.ImageID
	where su.StatusID=@StatusID
End
ELSE IF @StatusType='I'
Begin
	declare @imageID int
	insert into Images(ImageUrl,UserDetailsID,CreatedDate)
	values
	(@StatusContent,@UserDetailsID,@CreatedDate)
	select @imageID=@@IDENTITY
	
	insert into StatusUpdate(UserDetailsID,StatusType,StatusContent,createdDate)
	values(@UserDetailsID,@StatusType,@imageID,@CreatedDate)
	select @StatusID=@@IDENTITY
	
	select su.StatusID as StatusID,
	su.StatusType as StatusType,
	img1.ImageUrl as StatusContent,
	su.createdDate as CreatedDate,
	su.UserDetailsID as StatusByUserID,
	ud.FirstName+' '+ud.LastName as FullName,
	ud.proPicId as PicID,
	img2.ImageUrl as ProPicUrl from StatusUpdate as su
	join
	Images as img1
	on su.StatusContent=img1.ImageID
	join
	UserDetails as ud
	on su.UserDetailsID=ud.UserDetailsID
	join Images as img2
	on ud.proPicId=img2.ImageID
	where su.StatusID=@StatusID
	
End
ELSE IF @StatusType='V'
begin
	declare @vdoID int
	insert into Videos(VideoUrl,UserDetailsID,CreatedDate)
	values
	(@StatusContent,@UserDetailsID,@CreatedDate)
	select @vdoID=@@IDENTITY
	
	insert into StatusUpdate(UserDetailsID,StatusType,StatusContent,createdDate)
	values(@UserDetailsID,@StatusType,@vdoID,@CreatedDate)
	select @StatusID=@@IDENTITY
	
	select su.StatusID as StatusID,
	su.StatusType as StatusType,
	vdo.VideoUrl as StatusContent,
	su.createdDate as CreatedDate,
	su.UserDetailsID as StatusByUserID,
	ud.FirstName+' '+ud.LastName as FullName,
	ud.proPicId as PicId,
	img2.ImageUrl as ProPicUrl from StatusUpdate as su
	join
	Videos as vdo
	on su.StatusContent=vdo.VideoUrl
	join
	UserDetails as ud
	on su.UserDetailsID=ud.UserDetailsID
	join Images as img2
	on ud.proPicId=img2.ImageID
	where su.StatusID=@StatusID
end




go
/*******************************SAVE COMMENTS********************************/

alter proc Comments_SaveComment
(
	@StatusID int,
	@CommentedByID int,
	@CommentID int output,
	@CommentType varchar(10),
	@CommentContent varchar(500),
	@CreatedDate datetime
)

as

insert into Comments(CommentType,CommentContent,CommentedByID,StatusID,CreatedDate)
values
(@CommentType,@CommentContent,@CommentedByID,@StatusID,@CreatedDate)

select @CommentID=@@IDENTITY

go

/***********************Save Likes*******************************/

alter proc Likes_SaveLike
(
	@likedContentID int,
	@LikedByUSerID int,
	@LikID int output,
	@LikedContentType char(1),
	@CreatedDate datetime
)

as

insert into Likes(LikedByUserId,LikedContentId,LikedContentType,CreatedDate)
values
(@LikedByUSerID,@likedContentID,@LikedContentType,@CreatedDate)

select @LikID=@@IDENTITY

go