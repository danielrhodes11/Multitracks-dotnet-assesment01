TRUNCATE TABLE AssessmentSteps

INSERT INTO AssessmentSteps ([text]) 
VALUES ('Familiarize yourself with the code in the solution.')

INSERT INTO AssessmentSteps ([text]) 
VALUES ('Familiarize yourself with the data structures in the SQL project.')

INSERT INTO AssessmentSteps ([text]) 
VALUES ('Implement (Sync) Artist Details Page.<ul>')

INSERT INTO AssessmentSteps ([text]) 
VALUES ('Find markup in the folder multitracks.com-dotNet\Web\multitracks.com\multitracks.com\PageToSync')

INSERT INTO AssessmentSteps ([text]) 
VALUES ('Create a new page artistDetails.aspx using the provided markup')

INSERT INTO AssessmentSteps ([text]) 
VALUES ('Make the page data driven to display the appropriate images / text for a given parameter of artistID')

INSERT INTO AssessmentSteps ([text]) 
VALUES ('The page should pull all the needed data from a Stored Procedure (GetArtistDetails)')

INSERT INTO AssessmentSteps ([text]) 
VALUES ('The page should make use of the MTDataAccess Class Library. Look at the source of this page for an example. (ExecuteStoredProcedureDS will return a DataTable rather than a DataSet if multiple result sets are needed)</ul>')

INSERT INTO AssessmentSteps ([text]) 
VALUES ('Implement an API by adding a project under the Web folder in the solution. Use the technology of your choice here (MS Stack). The API should accomplish these tasks:<ul>')

INSERT INTO AssessmentSteps ([text]) 
VALUES ('Endpoint to search for an artist by name (api.multitracks.com/artist/search)')

INSERT INTO AssessmentSteps ([text]) 
VALUES ('Endpoint to list all songs with paging support using page size, page number, etc. (api.multitracks.com/song/list)')

INSERT INTO AssessmentSteps ([text]) 
VALUES ('Endpoint to add an Artist to the Artist table (api.multitracks.com/artist/add)</ul>')
