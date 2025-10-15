
import z from "zod";

export const activityObject = z.object({
  "id": z.string(),
  "title": z.string().min(2).max(200),
  "description": z.string().min(2).max(500),
  "eventDate": z.string(),
  "category": z.string().nonempty(),
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

export type LocationIQ = {
  place_id: string
  osm_id: string
  osm_type: string
  licence: string
  lat: string
  lon: string
  boundingbox: string[]
  class: string
  type: string
  display_name: string
  display_place: string
  display_address: string
  address: Address
}

export type Address = {
  name: string
  suburb: string
  city: string
  county: string
  state: string
  country: string
  country_code: string
}

export type LocationSuggestions = {
  label: string,
  id: string,
  lat: string,
  lon: string,
  city: string,
  venue: string
}