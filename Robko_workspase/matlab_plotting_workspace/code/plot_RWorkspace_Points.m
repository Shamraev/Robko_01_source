%This code plotting Robko 01 workspace by points%

%Run this file from Matlab (drug and drop in Matlab command window)
%or load this file folder in Matlab and run this code in Matlab (copypast)

[x,y,z] = getWorkspacePoints(15);  
figure('Name','Points Robko 01 workspace Plot','NumberTitle','off');
for i = 1:4
    subplot(2,2,i)
    plot3(x,y,z,'o', 'MarkerSize',3);
    if Constants.DO_CHESS_MWORKSPACE doChessManipulateWorkspace();end
    setView(i);
end
%print(gcf, '-dpdf', 'RWorkspace_Points.pdf', '-bestfit');
print(gcf, '-djpeg', '..\result\RWorkspace_Points.jpeg');



