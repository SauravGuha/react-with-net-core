
import z from "zod";

export const activityObject = z.object({
  "id": z.string(),
  "title": z.string().min(2).max(200),
  "description": z.string().min(2).max(500),
  "eventDate": z.string(),
  "category": z.string().nonempty(),
  "isCancelled": z.coerce.boolean(),
  "city": z.string().min(2).max(500),
  "venue": z.string().min(2).max(500),
  "latitude": z.coerce.number({
    error: "Latitude should be a number"
  }).min(-90).max(90),
  "longitude": z.coerce.number({
    error: "Longitude should be a number"
  }).min(-180).max(180)
});

export const activityResponseObject = z.object({
  "errorMessage": z.string().nullable(),
  "value": z.array(activityObject),
  "status": z.boolean(),
  "errorCode": z.number().nullable()

});

export type ActivityResponse = z.infer<typeof activityResponseObject>;
export type Activity = z.infer<typeof activityObject>;