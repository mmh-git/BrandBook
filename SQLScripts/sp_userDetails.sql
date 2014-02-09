use brandbook
go
/*Insert Default Image*/
--insert into Images(ImageUrl,CreatedDate)
--values
--('~/Content/Images/UserImages/DefaultImageMale.jpg',GETDATE()),
--('~/Content/Images/UserImages/DefaultImageFemale.jpg',GETDATE())
/***************************/

alter proc UserDetails_getUserDetails
(
	@UserId uniqueidentifier
)
as
select UserDetails.UserDetailsID as 'UserDetailsID',
UserDetails.UserId as 'UserId',
aspnet_Users.UserName as 'UserName',
FirstName,
LastName,
Address,
Designation,
Mobile,
Extension,
aspnet_Membership.Email as 'Email',
DOB,
Gender,
proPicId,
Images.ImageUrl as 'ImageUrl',
UserDetails.createdDate from UserDetails 
join
aspnet_Membership 
on UserDetails.UserId=aspnet_Membership.UserId
join Images
on
UserDetails.proPicId=images.ImageID
join
aspnet_Users 
on UserDetails.UserId=aspnet_Users.UserId
where UserDetails.UserId=@UserId
and Images.ImageID=UserDetails.proPicId

go


alter proc UserDetails_SaveUserDetails
(
@UserId uniqueidentifier,
@UserDetailsID int output,
@FirsName varchar(50),
@LasName	varchar(50),
@Designation varchar(20),
@DOB date,
@Gender varchar(6),
@proPicId int,
@createdDate date
)
as

select @createdDate=GETDATE()

insert into UserDetails(UserId,FirstName,LastName,Designation,DOB,Gender,proPicId,createdDate)
values(@UserId,@FirsName,@LasName,@Designation,@DOB,@Gender,@proPicId,@createdDate)

select @UserDetailsID=@@IDENTITY
go
