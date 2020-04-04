%This code plotting Robko 01 workspace by color points%

%Run this file from Matlab (drug and drop in Matlab command window)
%or load this file folder in Matlab and run this code in Matlab (copypast)

[x,y,z] = getWorkspacePoints(15);  
c = x+y-z;
figure('Name','Color points Robko 01 workspace Plot','NumberTitle','off'); 
for i = 1:4
    subplot(2,2,i)
    scatter3(x,y,z,5,c)
    setView(i);
end
%print(gcf, '-dpdf', 'RWorkspace_ColorPoints.pdf', '-bestfit');
print(gcf, '-djpeg', '..\result\RWorkspace_ColorPoints.jpeg');





