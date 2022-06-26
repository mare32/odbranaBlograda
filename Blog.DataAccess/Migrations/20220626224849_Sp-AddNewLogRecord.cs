using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataAccess.Migrations
{
    public partial class SpAddNewLogRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"create procedure [dbo].[AddNewLogRecord]
                    @useCaseName nvarchar(450),
                    @username nvarchar(max),
                    @userId int,
                    @executedAt datetime2(7),
                    @data nvarchar(max),
                    @isAuthorized bit
                    as
                    begin
                    insert into AuditLogs(UseCaseName,Username,UserId,ExecutedAt,Data,IsAuthorized)
                    values(@useCaseName,@username,@userId,@executedAt,@data,@isAuthorized) END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
