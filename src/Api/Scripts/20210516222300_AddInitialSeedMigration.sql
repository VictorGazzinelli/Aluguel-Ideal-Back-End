-- UP
INSERT INTO "public"."role" ("id","name") VALUES ('4cc1053a-b6af-11eb-8529-0242ac130003','Admin');INSERT INTO "public"."role" ("id","name") VALUES ('4cc1092c-b6af-11eb-8529-0242ac130003','Landlord');INSERT INTO "public"."role" ("id","name") VALUES ('4cc10b66-b6af-11eb-8529-0242ac130003','User');
INSERT INTO "public"."user" ("id","name","email","password") VALUES ('a298051c-b6af-11eb-8529-0242ac130003','Admin','admin@mail.com','8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92');INSERT INTO "public"."user" ("id","name","email","password") VALUES ('cc5e0018-44be-4bd4-8045-556541eb0b2e','Landlord','landlord@mail.com','8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92');INSERT INTO "public"."user" ("id","name","email","password") VALUES ('92145c8e-5f38-471b-9ded-e03b6c0a0767','User','user@mail.com','8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92');
INSERT INTO "public"."profile" ("user_id","role_id") VALUES ('a298051c-b6af-11eb-8529-0242ac130003','4cc1053a-b6af-11eb-8529-0242ac130003');INSERT INTO "public"."profile" ("user_id","role_id") VALUES ('a298051c-b6af-11eb-8529-0242ac130003','4cc1092c-b6af-11eb-8529-0242ac130003');INSERT INTO "public"."profile" ("user_id","role_id") VALUES ('a298051c-b6af-11eb-8529-0242ac130003','4cc10b66-b6af-11eb-8529-0242ac130003');INSERT INTO "public"."profile" ("user_id","role_id") VALUES ('cc5e0018-44be-4bd4-8045-556541eb0b2e','4cc1092c-b6af-11eb-8529-0242ac130003');INSERT INTO "public"."profile" ("user_id","role_id") VALUES ('cc5e0018-44be-4bd4-8045-556541eb0b2e','4cc10b66-b6af-11eb-8529-0242ac130003');INSERT INTO "public"."profile" ("user_id","role_id") VALUES ('92145c8e-5f38-471b-9ded-e03b6c0a0767','4cc10b66-b6af-11eb-8529-0242ac130003');
-- DOWN
DELETE FROM "public"."profile" WHERE "user_id" = 'a298051c-b6af-11eb-8529-0242ac130003' AND "role_id" = '4cc1053a-b6af-11eb-8529-0242ac130003';DELETE FROM "public"."profile" WHERE "user_id" = 'a298051c-b6af-11eb-8529-0242ac130003' AND "role_id" = '4cc1092c-b6af-11eb-8529-0242ac130003';DELETE FROM "public"."profile" WHERE "user_id" = 'a298051c-b6af-11eb-8529-0242ac130003' AND "role_id" = '4cc10b66-b6af-11eb-8529-0242ac130003';DELETE FROM "public"."profile" WHERE "user_id" = 'cc5e0018-44be-4bd4-8045-556541eb0b2e' AND "role_id" = '4cc1092c-b6af-11eb-8529-0242ac130003';DELETE FROM "public"."profile" WHERE "user_id" = 'cc5e0018-44be-4bd4-8045-556541eb0b2e' AND "role_id" = '4cc10b66-b6af-11eb-8529-0242ac130003';DELETE FROM "public"."profile" WHERE "user_id" = '92145c8e-5f38-471b-9ded-e03b6c0a0767' AND "role_id" = '4cc10b66-b6af-11eb-8529-0242ac130003';
DELETE FROM "public"."role" WHERE "id" = '4cc1053a-b6af-11eb-8529-0242ac130003' AND "name" = 'Admin';DELETE FROM "public"."role" WHERE "id" = '4cc1092c-b6af-11eb-8529-0242ac130003' AND "name" = 'Landlord';DELETE FROM "public"."role" WHERE "id" = '4cc10b66-b6af-11eb-8529-0242ac130003' AND "name" = 'User';
DELETE FROM "public"."user" WHERE "id" = 'a298051c-b6af-11eb-8529-0242ac130003' AND "name" = 'Admin' AND "email" = 'admin@mail.com' AND "password" = '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92';DELETE FROM "public"."user" WHERE "id" = 'cc5e0018-44be-4bd4-8045-556541eb0b2e' AND "name" = 'Landlord' AND "email" = 'landlord@mail.com' AND "password" = '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92';DELETE FROM "public"."user" WHERE "id" = '92145c8e-5f38-471b-9ded-e03b6c0a0767' AND "name" = 'User' AND "email" = 'user@mail.com' AND "password" = '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92';
