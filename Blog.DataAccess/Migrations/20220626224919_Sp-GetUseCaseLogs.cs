using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataAccess.Migrations
{
    public partial class SpGetUseCaseLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"create procedure [dbo].[GetUseCaseLogs]
                    @dateFrom datetime,
                    @dateTo datetime,
                    @useCaseName nvarchar(30),
                    @username nvarchar(max)
                    as
                    begin
                    select UseCaseName,Username,UserId,ExecutedAt,IsAuthorized,Data 
                    from AuditLogs
                    where ExecutedAt between @dateFrom and @dateTo AND
	                        (@useCaseName is null OR UseCaseName like '%'+@useCaseName+'%') AND
	                        (@username is null OR Username like '%'+@username+'%')
                    end";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
