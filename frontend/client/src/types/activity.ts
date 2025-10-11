
import z from "zod";

export const activityObject = z.object({
  "id": z.string(),
  "title": z.string(),
  "description": z.string(),
  "eventDate": z.string(),
  "category": z.string(),
  "isCancelled": z.coerce.boolean(),
  "city": z.string(),
  "venue": z.string(),
  "latitude": z.coerce.number(),
  "longitude": z.coerce.number()
});

export const activityResponseObject = z.object({
  "errorMessage": z.string().nullable(),
  "value": z.array(activityObject),
  "status": z.boolean(),
  "errorCode": z.number().nullable()

});

export type ActivityResponse = z.infer<typeof activityResponseObject>;
export type Activity = z.infer<typeof activityObject>;