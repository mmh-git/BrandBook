use brandbook

go
/**********All Status************/
alter proc statusUpdate_getStatusList
as
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

where su.StatusType='T'
union 
select 
temp.StatusID as StatusID,
temp.StatusType as StatusType,
temp.StatusContent as StatusContent,
temp.createdDate as CreatedDate,
temp.UserDetailsID as StatusByUserID,
temp.FirstName+' '+temp.LastName as FullName,
temp.proPicId as PicID,
img2.ImageUrl as ProPicUrl
 from 
(select su.StatusID,su.StatusType,su.StatusContent,su.createdDate,su.UserDetailsID,ud.FirstName,ud.LastName,ud.proPicId
from StatusUpdate as su
join
UserDetails as ud
on su.UserDetailsID=ud.UserDetailsID
where su.StatusType='I') as temp
join Images as img2
on temp.proPicId=img2.ImageID


union all

select 
temp.StatusID as StatusID,
temp.StatusType as StatusType,
temp.VideoUrl as StatusContent,
temp.createdDate as CreatedDate,
temp.UserDetailsID as StatusByUserID,
temp.FirstName+' '+temp.LastName as FullName,
temp.proPicId as PicID,
img2.ImageUrl as ProPicUrl
 from 
(select su.StatusID,su.StatusType,vid1.VideoUrl,su.createdDate,su.UserDetailsID,ud.FirstName,ud.LastName,ud.proPicId
from StatusUpdate as su
join
UserDetails as ud
on su.UserDetailsID=ud.UserDetailsID
join
Videos as vid1
on su.StatusContent=vid1.VideoID
where su.StatusType='V') as temp
join Images as img2
on temp.proPicId=img2.ImageID

order by su.createdDate desc 
/*************All Comments*****************/

select 
cmt.StatusID as StatusID,
cmt.CommentID as CommentID,
cmt.CommentContent as CommentContent,
cmt.CreatedDate as CreatedDate,
cmt.CommentType as CommentType,
cmt.CommentedByID as CommentedByUserID,
ud.FirstName+' '+ud.LastName as CommentedByUserFullName,
ud.proPicId as CommentedByUserProPicID,
img.ImageUrl as CommentedByUserProPicUrl
 from 
Comments as cmt
join 
UserDetails as ud
on cmt.CommentedByID=ud.UserDetailsID
join
Images as img
on ud.proPicId=img.ImageID
/*******************All Likes***********************/

select 
L.LikeID as LikeID,
L.LikedByUserId as LikedByUserID,
ud.FirstName+' '+ud.LastName as LikedByUserFullName,
L.LikedContentId as LikedContentID,
L.LikedContentType as LikedContentType,
L.CreatedDate as CreatedDate
from
Likes as L
join
UserDetails as ud
on L.LikedByUserId=ud.UserDetailsID