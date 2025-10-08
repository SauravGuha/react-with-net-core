
import z from "zod";

export const activityObject = z.object({
        "id" : z.string(),
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

export type Activity= z.infer<typeof activityObject>;