-- Adapted from https://gist.github.com/BManx2000/b04c64cb80046b577ba2
-- Execute this script from a mission and get the output in the log file

-- Limits that encompass the entire green area in the mission editor
local minY = -600000
local maxY = 1500000
local minX = -700000
local maxX = 400000
-- Data is sampled every 50km
local step = 50000

for xCoord = minX, maxX, step do
	local row = {}
	for yCoord = minY, maxY, step do
		local convLat, convLon = coord.LOtoLL({x = xCoord, y = 0, z = yCoord})
		local lookupPoint = {{yCoord, xCoord}, {convLat, convLon}}
		env.info(string.format("Y=%f,X=%f,Latitude=%f,Longitude=%f", yCoord, xCoord, convLat, convLon))
	end
end
