/* AluguelIdealDatabaseMigration migrating =================================== */

/* Beginning Transaction */
/* ExecuteSqlStatement DROP TABLE IF EXISTS advertisement */
DROP TABLE IF EXISTS advertisement
/* => 0,1692408s */

/* ExecuteSqlStatement DROP TABLE IF EXISTS contact */
DROP TABLE IF EXISTS contact
/* => 0,1592941s */

/* CreateTable advertisement */
CREATE TABLE "public"."advertisement" ("id" uuid NOT NULL, "title" varchar(255) NOT NULL, "deleted_at" timestamp, CONSTRAINT "PK_advertisement" PRIMARY KEY ("id"));
/* => 0,1789921s */

/* CreateTable contact */
CREATE TABLE "public"."contact" ("id" serial NOT NULL, "name" varchar(255) NOT NULL, "email" varchar(255) NOT NULL, "phone" varchar(255) NOT NULL, "deleted_at" timestamp, CONSTRAINT "PK_contact" PRIMARY KEY ("id"));
/* => 0,1944019s */

INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Ladislau Moraes','Pedro7@example.net','+555986102669',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Valentina Martins','Vitor_Macedo62@example.org','+559022187006',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Mariana Melo','Mariana.Reis@example.net','+556063916294',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Suélen Batista','Clara_Pereira50@example.net','+553326053121',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Sara Melo','Lucca.Xavier@example.org','+551567541410',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Sophia Pereira','Raul.Macedo37@example.net','+553849808611','2020-05-28T03:38:30');
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Yango Barros','Theo_Saraiva@example.com','+552911179518',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Paulo Xavier','Yago62@example.org','+558564209614',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Beatriz Nogueira','PedroHenrique65@example.org','+553295672236',NULL);
INSERT INTO "public"."contact" ("name","email","phone","deleted_at") VALUES ('Célia Oliveira','Morgana.Santos@example.org','+558121700870',NULL);
/* -> 10 Insert operations completed in 00:00:01.5898031 taking an average of 00:00:00.1589803 */
/* Committing Transaction */
/* AluguelIdealDatabaseMigration migrated */
/* => 0,3271926s */

