using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OsmPlayground.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,")
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "osm_relation_member_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_osm_relation_member_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "osm_users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    display_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    first_seen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_seen = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_osm_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "osm_nodes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    location = table.Column<Point>(type: "geometry(Point, 4326)", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false),
                    visible = table.Column<bool>(type: "boolean", nullable: false),
                    changeset = table.Column<long>(type: "bigint", nullable: false),
                    tags = table.Column<Dictionary<string, string>>(type: "hstore", nullable: false),
                    is_incomplete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_osm_nodes", x => x.id);
                    table.ForeignKey(
                        name: "fk_osm_nodes_osm_user_user_id",
                        column: x => x.user_id,
                        principalTable: "osm_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "osm_relations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false),
                    visible = table.Column<bool>(type: "boolean", nullable: false),
                    changeset = table.Column<long>(type: "bigint", nullable: false),
                    tags = table.Column<Dictionary<string, string>>(type: "hstore", nullable: false),
                    is_incomplete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_osm_relations", x => x.id);
                    table.ForeignKey(
                        name: "fk_osm_relations_osm_user_user_id",
                        column: x => x.user_id,
                        principalTable: "osm_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "osm_ways",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    geometry = table.Column<Geometry>(type: "geometry(Geometry, 4326)", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false),
                    visible = table.Column<bool>(type: "boolean", nullable: false),
                    changeset = table.Column<long>(type: "bigint", nullable: false),
                    tags = table.Column<Dictionary<string, string>>(type: "hstore", nullable: false),
                    is_incomplete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_osm_ways", x => x.id);
                    table.ForeignKey(
                        name: "fk_osm_ways_osm_users_user_id",
                        column: x => x.user_id,
                        principalTable: "osm_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "osm_relation_members",
                columns: table => new
                {
                    relation_id = table.Column<long>(type: "bigint", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    ref_id = table.Column<long>(type: "bigint", nullable: false),
                    role = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    node_ref_id = table.Column<long>(type: "bigint", nullable: true),
                    relation_ref_id = table.Column<long>(type: "bigint", nullable: true),
                    way_ref_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_osm_relation_members", x => new { x.relation_id, x.index });
                    table.CheckConstraint("ck_osm_relation_members_ref_matches_type", "(type = 1 AND (node_ref_id IS NULL OR node_ref_id = ref_id) AND way_ref_id IS NULL AND relation_ref_id IS NULL) OR (type = 2 AND (way_ref_id IS NULL OR way_ref_id = ref_id) AND node_ref_id IS NULL AND relation_ref_id IS NULL) OR (type = 3 AND (relation_ref_id IS NULL OR relation_ref_id = ref_id) AND node_ref_id IS NULL AND way_ref_id IS NULL)");
                    table.ForeignKey(
                        name: "fk_osm_relation_members_osm_nodes_node_ref_id",
                        column: x => x.node_ref_id,
                        principalTable: "osm_nodes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_osm_relation_members_osm_relation_member_type_lookup_type",
                        column: x => x.type,
                        principalTable: "osm_relation_member_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_osm_relation_members_osm_relations_relation_id",
                        column: x => x.relation_id,
                        principalTable: "osm_relations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_osm_relation_members_osm_relations_relation_ref_id",
                        column: x => x.relation_ref_id,
                        principalTable: "osm_relations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_osm_relation_members_osm_ways_way_ref_id",
                        column: x => x.way_ref_id,
                        principalTable: "osm_ways",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "osm_way_nodes",
                columns: table => new
                {
                    way_id = table.Column<long>(type: "bigint", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    node_id = table.Column<long>(type: "bigint", nullable: false),
                    node_ref_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_osm_way_nodes", x => new { x.way_id, x.index });
                    table.CheckConstraint("ck_osm_way_nodes_node_ref_matches_node_id", "node_ref_id IS NULL OR node_ref_id = node_id");
                    table.ForeignKey(
                        name: "fk_osm_way_nodes_osm_nodes_node_ref_id",
                        column: x => x.node_ref_id,
                        principalTable: "osm_nodes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_osm_way_nodes_osm_ways_way_id",
                        column: x => x.way_id,
                        principalTable: "osm_ways",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "osm_relation_member_types",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Node" },
                    { 2, "Way" },
                    { 3, "Relation" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_osm_nodes_location",
                table: "osm_nodes",
                column: "location")
                .Annotation("Npgsql:IndexMethod", "GIST");

            migrationBuilder.CreateIndex(
                name: "ix_osm_nodes_tags",
                table: "osm_nodes",
                column: "tags")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_hstore_ops" });

            migrationBuilder.CreateIndex(
                name: "ix_osm_nodes_user_id",
                table: "osm_nodes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_osm_relation_members_node_ref_id",
                table: "osm_relation_members",
                column: "node_ref_id");

            migrationBuilder.CreateIndex(
                name: "ix_osm_relation_members_relation_ref_id",
                table: "osm_relation_members",
                column: "relation_ref_id");

            migrationBuilder.CreateIndex(
                name: "ix_osm_relation_members_type",
                table: "osm_relation_members",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "ix_osm_relation_members_way_ref_id",
                table: "osm_relation_members",
                column: "way_ref_id");

            migrationBuilder.CreateIndex(
                name: "ix_osm_relations_tags",
                table: "osm_relations",
                column: "tags")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_hstore_ops" });

            migrationBuilder.CreateIndex(
                name: "ix_osm_relations_user_id",
                table: "osm_relations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_osm_way_nodes_node_ref_id",
                table: "osm_way_nodes",
                column: "node_ref_id");

            migrationBuilder.CreateIndex(
                name: "ix_osm_ways_geometry",
                table: "osm_ways",
                column: "geometry")
                .Annotation("Npgsql:IndexMethod", "GIST");

            migrationBuilder.CreateIndex(
                name: "ix_osm_ways_tags",
                table: "osm_ways",
                column: "tags")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_hstore_ops" });

            migrationBuilder.CreateIndex(
                name: "ix_osm_ways_user_id",
                table: "osm_ways",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "osm_relation_members");

            migrationBuilder.DropTable(
                name: "osm_way_nodes");

            migrationBuilder.DropTable(
                name: "osm_relation_member_types");

            migrationBuilder.DropTable(
                name: "osm_relations");

            migrationBuilder.DropTable(
                name: "osm_nodes");

            migrationBuilder.DropTable(
                name: "osm_ways");

            migrationBuilder.DropTable(
                name: "osm_users");
        }
    }
}
