-- UP
DROP TABLE "public"."flat";
DROP TABLE "public"."house";
CREATE TABLE "public"."flat" ("id" uuid NOT NULL, "condominium" float8 NOT NULL, "floor" integer NOT NULL);
ALTER TABLE "public"."flat" ADD CONSTRAINT "FK_flat_id_residence_id" FOREIGN KEY ("id") REFERENCES "public"."residence" ("id");
ALTER TABLE "public"."flat" ADD CONSTRAINT "PK_flat" PRIMARY KEY ("id");
CREATE TABLE "public"."house" ("id" uuid NOT NULL, "yard_area" float8 NOT NULL);
ALTER TABLE "public"."house" ADD CONSTRAINT "FK_house_id_residence_id" FOREIGN KEY ("id") REFERENCES "public"."residence" ("id");
ALTER TABLE "public"."house" ADD CONSTRAINT "PK_house" PRIMARY KEY ("id");
-- DOWN
DROP TABLE "public"."house";
DROP TABLE "public"."flat";
CREATE TABLE "public"."house" ("id" uuid NOT NULL, "residence_id" uuid NOT NULL, "yard_area" float8 NOT NULL, PRIMARY KEY ("id"));
ALTER TABLE "public"."house" ADD CONSTRAINT "FK_house_residence_id_residence_id" FOREIGN KEY ("residence_id") REFERENCES "public"."residence" ("id");
CREATE TABLE "public"."flat" ("id" uuid NOT NULL, "residence_id" uuid NOT NULL, "condominium" float8 NOT NULL, "floor" integer NOT NULL, PRIMARY KEY ("id"));
ALTER TABLE "public"."flat" ADD CONSTRAINT "FK_flat_residence_id_residence_id" FOREIGN KEY ("residence_id") REFERENCES "public"."residence" ("id");
