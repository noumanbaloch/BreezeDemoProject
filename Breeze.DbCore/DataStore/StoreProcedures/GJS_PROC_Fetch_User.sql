USE [GulfJobsDB]
GO
/****** Object:  StoredProcedure [dbo].[GJS_PROC_Fetch_User]    Script Date: 10/29/2022 9:12:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nouman Baloch>
-- Create date: <08/18/2022>
-- Description:	<Fetch User Info>
-- =============================================
ALTER PROCEDURE [dbo].[GJS_PROC_Fetch_User]
	-- Add the parameters for the stored procedure here
	@Username varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT UserName from users where UserName = @Username and Deleted = 0
END
