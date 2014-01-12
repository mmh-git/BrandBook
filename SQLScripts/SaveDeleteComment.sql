use brandbook

go

ALTER PROC SaveDeleteComment 
(
	@CommentID INT,
	@StatusID INT,
	@CommentedByID INT,
	@CommentType VARCHAR(1),
	@CommentContent nvarchar(500),
	@Action VARCHAR(1)
)
AS
IF @Action='I'
	BEGIN
		INSERT INTO Comments(StatusID,CommentedByID,CommentType,CommentContent,CreatedDate)
		VALUES(@StatusID,@CommentedByID,@CommentType,@CommentContent,GETDATE())
		SELECT @CommentID=@@IDENTITY
	END	
ELSE IF @Action='U'
	BEGIN
		UPDATE Comments SET CommentContent=@CommentContent WHERE CommentID=@CommentID
	END
ELSE IF @Action='D'
	BEGIN
		DELETE Comments WHERE CommentID=@CommentID
	END
	
SELECT c.CommentID,c.StatusID,
c.CommentedByID,c.CommentContent,c.CreatedDate
,c.CommentType,
ud.FirstName+' '+
ud.LastName as CommentedByUserFullName,
i.ImageID,
i.ImageUrl FROM Comments AS c
JOIN
UserDetails as ud
ON c.CommentedByID=ud.UserDetailsID
JOIN
Images AS i
ON ud.proPicId=I.ImageID
WHERE C.CommentID=@CommentID

