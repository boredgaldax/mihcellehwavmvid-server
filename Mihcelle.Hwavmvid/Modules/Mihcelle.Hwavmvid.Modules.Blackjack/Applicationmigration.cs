﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Mihcelle.Hwavmvid.Data;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Modules.Blackjack
{

    [DbContext(typeof(Applicationdbcontext))]
    [Migration("mihcelle.hwavmvid.modules.blackjack.01.00.00.00")]
    public class Applicationmigration : Migration
    {

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            base.BuildTargetModel(modelBuilder);
        }

        protected override void Up(MigrationBuilder migrationbuilder)
        {

            migrationbuilder.CreateTable(
                 name: "Applicationblackjacks",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Moduleid = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_blackjackid", item => item.Id);
                     dbtable.ForeignKey("fk_application_blackjack_moduleid", item => item.Moduleid, "Applicationmodules", "Id");
                 });
        }

        protected override void Down(MigrationBuilder migrationbuilder)
        {

            migrationbuilder.DropColumn("Applicationblackjacks", "Applicationblackjacks");

        }
    }
}
