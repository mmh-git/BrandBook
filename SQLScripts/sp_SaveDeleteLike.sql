use brandbook
go
ALTER PROC sp_SaveDeleteLike
(
	@LikedContentId INT,
	@LikeID INT,
	@UserID INT,
	@LikedContentType CHAR(1)
)
AS

IF @LikeID=0
BEGIN
	INSERT INTO Likes(LikedContentType,LikedContentId,LikedByUserId,CreatedDate)
	VALUES(@LikedContentType,@LikedContentId,@UserID,GETDATE())
	SELECT @LikeID=@@IDENTITY
END
ELSE
BEGIN
	DELETE Likes WHERE LikeID=@LikeID
END

SELECT LikeID,LikedContentId,LikedContentType,LikedByUserId,l.CreatedDate,ud.FirstName FROM Likes as l
	join UserDetails as ud
	on l.LikedByUserId=ud.UserDetailsID
	 WHERE LikedContentId=@LikedContentId 