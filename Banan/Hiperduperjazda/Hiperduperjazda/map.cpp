#include "map.h"

void Map::GenerateMap(int mapColor, MAP_TYPE mtype, int min_rows, int min_platforms, int max_rows, int max_platforms)
{
	elements.clear();
	color = mapColor;
	if(mtype == RANDOM)
	{
		mtype = (MAP_TYPE)(GetRandomNumber(4));
	}
	type = mtype;
	GenerateBorders(mtype);
	GenerateRandomMap(min_rows, min_platforms, max_rows, max_platforms);
}

void Map::GenerateBorders(MAP_TYPE type)
{
	SDL_Rect lewa, prawa, gora, dol;
	lewa.x = 0; lewa.y = -2*SCREEN_HEIGHT; lewa.h = SCREEN_HEIGHT*5; lewa.w = 20;
	prawa.x = SCREEN_WIDTH - 20; lewa.y = -2*SCREEN_HEIGHT; lewa.h = SCREEN_HEIGHT*5; lewa.w = 20;
	gora.x = -2*SCREEN_WIDTH; gora.y = 0; gora.h = 20; gora.w = SCREEN_WIDTH*5;
	dol.x = -2*SCREEN_WIDTH; dol.y = SCREEN_HEIGHT - 20; dol.h = 20; dol.w = SCREEN_WIDTH*5;
	
	switch(type)
	{
	case OPENED:
		break;
	case CLOSED:
		elements.push_back(gora);
		elements.push_back(dol);
		elements.push_back(lewa);
		elements.push_back(prawa);
		break;
	case HORIZONTAL_CLOSED:
		elements.push_back(gora);
		elements.push_back(dol);
		break;
	case VERTICAL_CLOSED:
		elements.push_back(lewa);
		elements.push_back(prawa);
		break;
	}
}

void Map::GenerateRandomMap(int min_rows, int min_platforms, int max_rows, int max_platforms) 
{
	int i, k;
	int offset_x = 0;

	int offset_1 = 0;
	int platform_width = 0; 
	int offset_2 = 0;

	SDL_Rect element;
	do{
		rows = GetRandomNumber(max_rows, 1);
	} while (rows < min_rows);

	for(i = 0; i < rows; i++)
	{
		offset_x = 0;
		do {
			platforms = GetRandomNumber(max_platforms, 1);
		} while(platforms < min_platforms);
		
		for(k = 0; k < platforms; k++)
		{
			do {
				offset_1 = GetRandomNumber(SCREEN_WIDTH/3);
			} while(offset_1 < MIN_SPACE*platforms/2);
			do {
				offset_2 = GetRandomNumber(SCREEN_WIDTH/3);
			} while(offset_2 < MIN_SPACE*platforms/2);
			platform_width = SCREEN_WIDTH - (offset_1 + offset_2);
			offset_1 /= platforms; offset_2 /= platforms; platform_width /= platforms;

			element.x = offset_x + offset_1;
			element.w = platform_width;
			element.y = (SCREEN_HEIGHT/rows) * (i+1) - 20;
			element.h = 20;
			offset_x += offset_1 + offset_2 + platform_width;

			elements.push_back(element);
		}
	}
}

void Map::Draw(SDL_Surface* surface)
{
	for(unsigned int i = 0; i < elements.size();i++)
	{
		SDL_FillRect(surface, &elements.at(i), color);
	}
}

std::vector<SDL_Surface*> Map::MapInfo()
{
	SDL_Surface* message1;
	std::vector<SDL_Surface*> vector;
	std::ostringstream temp;

	temp.str(std::string());
	temp << "MAP:    Type: " << (int)type << ", rows: " << rows << ", platforms: " << platforms;
	message1 = TTF_RenderText_Solid(font, temp.str().c_str(), textColor);
	vector.push_back(message1);
	
	return vector;
}

