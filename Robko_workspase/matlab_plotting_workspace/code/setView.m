function setView(i)
    titleName = {'Isometric View','View Normal to X-Y Plane', 'View Normal to X-Z Plane', 'View Normal to Y-Z Plane'};
    
     if i == 2 view(0,90);
     elseif i == 3 view(0,0);
     elseif i == 4 view(90,0);
     else
     end
    title(titleName(i))
    xlabel('X')
    ylabel('Y')
    zlabel('Z')        
end

