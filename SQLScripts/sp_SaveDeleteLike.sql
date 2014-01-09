use brandbook
go
ALTER PROC sp_SaveDeleteLike
(
	@LikedContentId INT,
	@UserID INT,
	@LikedContentType VARCHAR(1)
)
AS

IF NOT EXISTS (SELECT * FROM Likes WHERE LikedContentId=@LikedContentId AND LikedByUserId=@UserID) 
BEGIN
	INSERT INTO Likes(LikedContentType,LikedContentId,LikedByUserId,CreatedDate)
	VALUES(@LikedContentType,@LikedContentId,@UserID,GETDATE())
END
ELSE
BEGIN
	DELETE Likes WHERE LikedContentId=@LikedContentId and LikedByUserId=@UserID
END

SELECT LikeID,LikedContentId,LikedContentType,LikedByUserId,l.CreatedDate,ud.FirstName FROM Likes as l
	join UserDetails as ud
	on l.LikedByUserId=ud.UserDetailsID
	 WHERE LikedContentId=@LikedContentId 
	 
	 