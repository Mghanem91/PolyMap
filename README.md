# PolyMap
# Export:
{server}/api/Polymap/Export?data=1,2,3

# Post new 
request type :post 

url : {server}/api/Polymap/post

sample data :
{
  "UserId":"1002",
  "CoordinateList":
[
  {"lat":31.89964,"lng":35.20422},
  {"lat":32.18966,"lng":34.97063},
  {"lat":32.22111,"lng":35.25444},
  {"lat":31.89964,"lng":35.20422},
  {"lat":32.22111,"lng":35.25444}
]
}

# Delete polygon
request type :delete 

url : {server}/api/Polymap/delete/{id}

# Notes
sql files to create table and insert dummy data exists on teh root.
Performance may depend on internet connection because we fitched data from external links.
