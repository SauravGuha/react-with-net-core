
import z from "zod";

const activityObject = z.object({
        "id" : z.string(),
        "title": z.string(),
        "description": z.string(),
        "eventDate": z.string(),
        "category": z.string(),
        "isCancelled": z.boolean(),
        "city": z.string(),
        "venue": z.string(),
        "latitude": z.number(),
        "longitude": z.number()
  });

export type Activity= z.infer<typeof activityObject>;