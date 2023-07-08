CREATE PROCEDURE dbo.GetAssessmentSteps
	@stepID INT = -1
AS
BEGIN

	SELECT [text]
	FROM AssessmentSteps
	WHERE
		@stepID = -1 OR
		stepID = @stepID

END
